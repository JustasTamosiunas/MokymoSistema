import { TableContainer, TableRow } from "@mui/material";
import Button from "@mui/material/Button";
import Paper from "@mui/material/Paper";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableHead from "@mui/material/TableHead";
import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { CreateGrade, DeleteGrade, EditGrade, GetGrades } from "../../api/CoursesApi";
import IGrade from "../../models/IGrade";
import useToken from "../auth/usetoken";
import CreateGradeDialog from "./createGradeDialog";
import DeleteGradeDialog from "./deleteGradeDialog";
import EditGradeDialog from "./editGradeDialog";

export default function Grades(){
    let {courseId, lectureId} = useParams()
    const [data, setData] = useState(Array<IGrade>)
    const {token, setToken} = useToken()

    const [isCreateGradeDialogVisible, setIsCreateGradeDialogVisible] = useState(false)
    const [isEditGradeDialogOpen, setEditGradeDialogOpen] = useState(false)
    const [isDeleteGradeDialogOpen, setDeleteGradeDialogOpen] = useState(false)
    const [elementToDelete, setElementToDelete] = useState(-1)
    const [elementToEdit, setElementToEdit] = useState(-1)
    const [studentNameToEdit, setStudentNameToEdit] = useState('')
    const [gradeToEdit, setGradeToEdit] = useState(-1)

    function createGrade(agree: boolean, studentName: string, grade: number){
        if (agree) {
            CreateGrade(token, courseId ?? "", lectureId ?? "", studentName, grade).then(() => {
                GetGrades(token, courseId ?? "", lectureId ?? "").then(setData)
            })
        }
        setIsCreateGradeDialogVisible(false)
    }

    function editGrade(agree: boolean, studentName: string, grade: number){
        if (agree && courseId){
            EditGrade(token, courseId!, lectureId!, data[elementToEdit].id, studentName, grade).then(() => {
                    GetGrades(token, courseId ?? "", lectureId ?? "").then(setData)
            })
        }
        setEditGradeDialogOpen(false)
    }

    function deleteGrade(agree: boolean){
        if (agree) {
            DeleteGrade(token, courseId!, lectureId!, data[elementToDelete].id).then((response) => {
                setData([
                    ...data.slice(0, elementToDelete),
                    ...data.slice(elementToDelete + 1)
                ]);
            })
        }
        setDeleteGradeDialogOpen(false)
    }

    useEffect(() => {
        GetGrades(token, courseId!, lectureId!).then(setData)
    }, [])

    return(
        <TableContainer component={Paper} sx={{maxWidth: "80%"}}>
            <Button onClick={() => {setIsCreateGradeDialogVisible(true)}}>Add a grade</Button>
            <Table>
                <TableHead>
                    <TableRow>
                        <TableCell>Student Name</TableCell>
                        <TableCell>Grade</TableCell>
                        <TableCell>Actions</TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {data.map((grade, index) => (
                        <TableRow>
                            <TableCell>{grade.studentName}</TableCell>
                            <TableCell>{grade.result}</TableCell>
                            <TableCell>
                                <Button onClick={() => {
                                    setElementToEdit(index)
                                    setStudentNameToEdit(grade.studentName)
                                    setGradeToEdit(grade.result)
                                    setEditGradeDialogOpen(true)
                                    }}
                                >
                                    Edit
                                </Button>
                                <Button onClick={() => {
                                    setDeleteGradeDialogOpen(true)
                                    setElementToDelete(index)
                                    }}
                                >
                                    Delete
                                </Button>
                            </TableCell>
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
            <CreateGradeDialog createGrade={createGrade} isOpen={isCreateGradeDialogVisible}/>
            <EditGradeDialog editLecture={editGrade} isOpen={isEditGradeDialogOpen} studentName={studentNameToEdit} grade={gradeToEdit}/>
            <DeleteGradeDialog deleteGrade={deleteGrade} isOpen={isDeleteGradeDialogOpen}/>
        </TableContainer>
    )
}