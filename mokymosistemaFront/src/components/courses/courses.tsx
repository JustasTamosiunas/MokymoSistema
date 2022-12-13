import { useEffect, useState } from "react";
import ICourse from "../../models/ICourse";
import Card from '@mui/material/Card';
import CardActions from '@mui/material/CardActions';
import CardContent from '@mui/material/CardContent';
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';
import GetCourses, { CreateCourse, DeleteCourse, EditCourse, GetRoles } from "../../api/CoursesApi";
import useToken from "../auth/usetoken";
import AlertDialog from "./courseDeleteDialog";
import CourseCreateDialog from "./courseCreateDialog";
import CourseEditDialog from "./courseEditDialog";
import Grid2 from "@mui/material/Unstable_Grid2/Grid2";
import { CardActionArea } from "@mui/material";

function Courses() {

    const [courses, setCourses] = useState(Array<ICourse>)
    const [deleteDialogOpen, setDeleteDialogOpen] = useState(false)
    const [createDialogOpen, setCreateDialogOpen] = useState(false)
    const [editDialogOpen, setEditDialogOpen] = useState(false)
    const [elementToDelete, setElementToDelete] = useState(-1)
    const [elementToEdit, setElementToEdit] = useState(-1)
    const {token, setToken} = useToken();
    const [roles, setRoles] = useState(Array<string>)

    useEffect(() => {
        GetCourses().then(setCourses)
        GetRoles(token).then((response) => setRoles(response.data))
    }, [])

    function deleteCourse(agree: boolean){
        if (agree) {
            DeleteCourse(courses[elementToDelete].id, token ?? '').then((response) => {
                setCourses([
                    ...courses.slice(0, elementToDelete),
                    ...courses.slice(elementToDelete + 1)
                ]);
            })
        }
        setDeleteDialogOpen(false)
    }

    function createCourse(agree: boolean, name: string){
        if (agree){
            CreateCourse(token, name).then(() => {GetCourses().then(setCourses)})
        }
        setCreateDialogOpen(false)
    }

    function editCourse(agree: boolean, name: string){
        if (agree){
            EditCourse(token, courses[elementToEdit].id, name).then(() => {GetCourses().then(setCourses)})
        }
        setEditDialogOpen(false)
    }

    return (
        <div>
            { roles.includes("Admin") &&
            <Button onClick={() => {
                setCreateDialogOpen(true)
            }}>
                Create New Course
            </Button>
            }
            <Grid2 container spacing={{ xs: 2, md: 3 }} columns={{ xs: 4, sm: 8, md: 12 }}>
            {courses.map((course, index) => {
                return <Grid2 xs={4} sm={4} md={4} key={index}>
                    <Card key={index} sx={{ minWidth: 275, margin: 5 }}>
                        <CardActionArea href={`/courses/${course.id}/lectures`}>
                            <CardContent>
                                <Typography sx={{ fontSize: 14 }} color="text.secondary" gutterBottom>
                                Course
                                </Typography>
                                <Typography variant="h5" component="div">
                                {course.name}
                                </Typography>
                            </CardContent>
                        </CardActionArea>
                        {roles.includes("Admin") &&
                        <CardActions>
                            <Button size="small"onClick={() => {
                                setEditDialogOpen(true)
                                setElementToEdit(index)}}>Edit</Button>
                            <Button size="small" color="error" onClick={() => {
                                setDeleteDialogOpen(true)
                                setElementToDelete(index)}}>Delete</Button>
                        </CardActions>}
                    </Card>
                </Grid2>
            })}
            </Grid2>
            <AlertDialog deleteCourse={deleteCourse} isOpen={deleteDialogOpen}/>
            <CourseCreateDialog createCourse={createCourse} isOpen={createDialogOpen}/>
            <CourseEditDialog editCourse={editCourse} isOpen={editDialogOpen}/>
        </div>
      );
}

export default Courses