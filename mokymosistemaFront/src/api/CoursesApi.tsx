import ICourse from "../models/ICourse";
import axios from "axios";
import ILoginRequestDTO from "../models/ILoginRequestDTO";
import ILecture from "../models/ILecture";
import IGrade from "../models/IGrade";

export default function GetCourses(){
    return axios.get<Array<ICourse>>("https://localhost:7210/api/courses").then((response) => {
        return response.data;
    })
}

export function DeleteCourse(id: number, token: string){
    return axios.delete("https://localhost:7210/api/courses/" + id, {
        headers: {
            'Authorization': `Bearer ${token}`
        }
    }).then((response) => {
        return response.status
    })
}

export function LoginRequest(credentials: ILoginRequestDTO){
    return axios.post("https://localhost:7210/api/login", credentials).then((response) => {
        return response.data
    })
}

export function GetRoles(token: string){
    return axios.get("https://localhost:7210/api/roles/" , {
        headers: {
            'Authorization': `Bearer ${token}`
        }
    })
}

export function CreateCourse(token: string, name: string){
    return axios.post("https://localhost:7210/api/courses/" ,
    {
        name: name,
    },
    {
        headers: {
        'Authorization': `Bearer ${token}`
        }
    })
}

export function EditCourse(token: string, id: number, name: string){
    return axios.put("https://localhost:7210/api/courses/" + id ,
    {
        name: name,
    },
    {
        headers: {
        'Authorization': `Bearer ${token}`
        }
    })
}

export function GetLectures(token: string, courseId: number){
    return axios.get<Array<ILecture>>(`https://localhost:7210/api/courses/${courseId}/lectures`,
    {
        headers: {
            'Authorization': `Bearer ${token}`
        }
    }).then((response) => {
        return response.data;
    })
}

export function CreateLecture(token: string, courseId: number, material: string, assignment: string){
    return axios.post(`https://localhost:7210/api/courses/${courseId}/lectures` ,
    {
        material: material,
        assignment: assignment
    },
    {
        headers: {
        'Authorization': `Bearer ${token}`
        }
    })
}

export function EditLecture(token: string, courseId: number, lectureId: number, material: string, assignment: string){
    return axios.put(`https://localhost:7210/api/courses/${courseId}/lectures/${lectureId}` ,
    {
        material: material,
        assignment: assignment
    },
    {
        headers: {
        'Authorization': `Bearer ${token}`
        }
    })
}

export function DeleteLecture(token: string, courseId: number, lectureId: number,){
    return axios.delete(`https://localhost:7210/api/courses/${courseId}/lectures/${lectureId}`,
    {
        headers: {
        'Authorization': `Bearer ${token}`
        }
    })
}

export function GetGrades(token: string, courseId: string, lectureId: string){
    return axios.get<Array<IGrade>>(`https://localhost:7210/api/courses/${courseId}/lectures/${lectureId}/grades`,
    {
        headers: {
            'Authorization': `Bearer ${token}`
        }
    }).then((response) => {
        return response.data;
    })
}

export function CreateGrade(token: string, courseId: string, lectureId: string, studentName: string, grade: number){
    return axios.post(`https://localhost:7210/api/courses/${courseId}/lectures/${lectureId}/grades` ,
    {
        studentName: studentName,
        grade: grade
    },
    {
        headers: {
        'Authorization': `Bearer ${token}`
        }
    })
}

export function EditGrade(token: string, courseId: string, lectureId: string, gradeId: number, studentName: string, grade: number){
    return axios.put(`https://localhost:7210/api/courses/${courseId}/lectures/${lectureId}/grades/${gradeId}` ,
    {
        studentName: studentName,
        grade: grade
    },
    {
        headers: {
        'Authorization': `Bearer ${token}`
        }
    })
}

export function DeleteGrade(token: string, courseId: string, lectureId: string, gradeId: number){
    return axios.delete(`https://localhost:7210/api/courses/${courseId}/lectures/${lectureId}/grades/${gradeId}` ,
    {
        headers: {
        'Authorization': `Bearer ${token}`
        }
    })
}