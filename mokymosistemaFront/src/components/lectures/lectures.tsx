import { Stack } from "@mui/system";
import { useEffect, useState } from "react";
import { CreateLecture, DeleteLecture, EditLecture, GetLectures } from "../../api/CoursesApi";
import ILecture from "../../models/ILecture";
import useToken from "../auth/usetoken";
import CardContent from '@mui/material/CardContent';
import Card from '@mui/material/Card';
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';
import Divider from '@mui/material/Divider';
import CardActions from '@mui/material/CardActions';
import Container from '@mui/material/Container';
import { useParams } from "react-router-dom";
import CreateLectureDialog from "./createLectureDialog";
import EditLectureDialog from "./editLectureDialog";
import DeleteLectureDialog from "./deleteLectureDialog";

export default function Lectures() {
    const {token, setToken} = useToken();
    const [data, setData] = useState(Array<ILecture>)
    const [isLectureCreateDialogOpen, setLectureCreateDialogOpen] = useState(false)
    const [isLectureEditDialogOpen, setLectureEditDialogOpen] = useState(false)
    const [isLectureDeleteDialogOpen, setLectureDeleteDialogOpen] = useState(false)
    const [elementToDelete, setElementToDelete] = useState(-1)
    const [elementToEdit, setElementToEdit] = useState(-1)
    const [materialToEdit, setMaterialToEdit] = useState('')
    const [assignmentToEdit, setAssignmentToEdit] = useState('')
    let {courseId} = useParams();

    useEffect(() => {
        if (courseId)
            GetLectures(token, parseInt(courseId)).then(setData)
    }, [])

    function createLecture(agree: boolean, material: string, assignment: string){
        if (agree && courseId){
            CreateLecture(token, parseInt(courseId), material, assignment).then(() => {
                if (courseId)
                    GetLectures(token, parseInt(courseId)).then(setData)
            })
        }
        setLectureCreateDialogOpen(false)
    }

    function editLecture(agree: boolean, material: string, assignment: string){
        if (agree && courseId){
            EditLecture(token, parseInt(courseId), data[elementToEdit].id, material, assignment).then(() => {
                if (courseId)
                    GetLectures(token, parseInt(courseId)).then(setData)
            })
        }
        setLectureEditDialogOpen(false)
    }

    function deleteLecture(agree: boolean){
        if (agree) {
            DeleteLecture(token, parseInt(courseId ?? ''), data[elementToDelete].id).then((response) => {
                setData([
                    ...data.slice(0, elementToDelete),
                    ...data.slice(elementToDelete + 1)
                ]);
            })
        }
        setLectureDeleteDialogOpen(false)
    }

    return (
        <div style={{marginTop: "100px", width: "100%", marginBottom: "100px"}}>
            <Container>
                <Button onClick={() => {
                    setLectureCreateDialogOpen(true)
                }}>
                    Add a lecture
                </Button>
                <Stack
                    direction="column"
                    justifyContent="flex-start"
                    alignItems="center"
                    spacing={2}
                    >
                        {data.map((lecture, index) => {
                            return <Card key={index} sx={{width: "100%", textAlign: "left"}}>
                                        <CardContent>
                                            <Typography variant="subtitle2" color="text.primary">
                                                Lecture {index + 1}
                                            </Typography>
                                            <Typography variant="subtitle1" color="text.secondary" paragraph={true}>
                                                Material
                                            </Typography>
                                            <Typography variant="body1" paragraph={true}>
                                                {lecture.material}
                                            </Typography>
                                            <Divider sx={{pt: "15px"}} />
                                            <Typography variant="subtitle1" color="text.secondary">
                                                Assignment
                                            </Typography>
                                            <Typography variant="body1" paragraph={true}>
                                                {lecture.assignment}
                                            </Typography>
                                        </CardContent>
                                        <CardActions>
                                            <Button onClick={() => {
                                                setElementToEdit(index)
                                                setMaterialToEdit(lecture.material)
                                                setAssignmentToEdit(lecture.assignment)
                                                setLectureEditDialogOpen(true)
                                                }}
                                            >
                                                Edit
                                            </Button>
                                            <Button onClick={() => {
                                                setLectureDeleteDialogOpen(true)
                                                setElementToDelete(index)
                                                }}
                                            >
                                                Delete
                                            </Button>
                                            <Button href={`/courses/${courseId}/lectures/${lecture.id}/grades`}>
                                                View Grades
                                            </Button>
                                        </CardActions>
                                    </Card>
                        })}
                </Stack>
            </Container>
            <CreateLectureDialog createLecture={createLecture} isOpen={isLectureCreateDialogOpen}/>
            <EditLectureDialog editLecture={editLecture} isOpen={isLectureEditDialogOpen} material={materialToEdit} assignment={assignmentToEdit}/>
            <DeleteLectureDialog deleteLecture={deleteLecture} isOpen={isLectureDeleteDialogOpen}/>
        </div>
    )
}