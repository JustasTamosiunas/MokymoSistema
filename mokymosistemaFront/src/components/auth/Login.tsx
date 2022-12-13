import { useState } from "react";
import { LoginRequest } from "../../api/CoursesApi";
import useToken from "./usetoken";

interface LoginProps {
    setToken: any
}

export default function Login(props: LoginProps) {
    const [username, setUserName] = useState('');
    const [password, setPassword] = useState('');
    const {token, setToken} = useToken()

    const handleSubmit = async (e: { preventDefault: () => void; }) => {
        e.preventDefault();
        LoginRequest({username, password}).then((response) => {
            console.log(response)
            props.setToken(response['token'])
        })
    }
    
    return(
      <form onSubmit={handleSubmit}>
        <label>
          <p>Username</p>
          <input type="text" onChange={e => setUserName(e.target.value)}/>
        </label>
        <label>
          <p>Password</p>
          <input type="password" onChange={e => setPassword(e.target.value)}/>
        </label>
        <div>
          <button type="submit">Submit</button>
        </div>
      </form>
    )
  }
