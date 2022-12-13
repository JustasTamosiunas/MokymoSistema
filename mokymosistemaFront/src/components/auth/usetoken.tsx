import { useState } from 'react';

export default function useToken() {
    const getToken = () => {
    const tokenString = localStorage.getItem('token');
    if (tokenString != null){
        const userToken = tokenString;
        return userToken
    }
    return ""
    };

  const [token, setToken] = useState(getToken());

  const saveToken = (userToken: string) => {
    localStorage.setItem('token', userToken);
    setToken(userToken);
  };

  return {
    setToken: saveToken,
    token
  }

}