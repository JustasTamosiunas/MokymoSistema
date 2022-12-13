import { Button, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle, TextField, ToggleButton, ToggleButtonGroup } from '@mui/material';
import * as React from 'react';

export default function EditGradeDialog(props){
    const [studentName, setStudentName] = React.useState('')
    const [grade, setGrade] = React.useState(0)

    React.useEffect(() => {
      setStudentName(props.studentName)
      setGrade(props.grade)
    }, [props.studentName, props.grade])
    
    function handleStudentNameChange(event: React.ChangeEvent<HTMLInputElement>){
      setStudentName(event.target.value)
    }

    function handleGradeChange (event: React.MouseEvent<HTMLElement>, newGrade: number,) {
        setGrade(newGrade);
    };

    return (
      <div>
        <Dialog open={props.isOpen} onClose={() => {props.editLecture(false); setStudentName(''); setGrade(-1)}}>
          <DialogTitle>Edit Grade</DialogTitle>
          <DialogContent>
            <DialogContentText>
                The name of the student
            </DialogContentText>
            <TextField
                autoFocus
                margin="dense"
                id="name"
                label="Student name"
                type="text"
                fullWidth
                variant="outlined"
                value={studentName}
                onChange={handleStudentNameChange}
                disabled
            />

            <DialogContentText sx={{marginTop: "5px", marginBottom: "10px"}}>
                Select the grade
            </DialogContentText>
            <ToggleButtonGroup
                color="primary"
                value={grade}
                exclusive
                onChange={handleGradeChange}
                size="large"
                sx={{display: "flex"}}
            >
                <ToggleButton value={1}>1</ToggleButton>
                <ToggleButton value={2}>2</ToggleButton>
                <ToggleButton value={3}>3</ToggleButton>
                <ToggleButton value={4}>4</ToggleButton>
                <ToggleButton value={5}>5</ToggleButton>
                <ToggleButton value={6}>6</ToggleButton>
                <ToggleButton value={7}>7</ToggleButton>
                <ToggleButton value={8}>8</ToggleButton>
                <ToggleButton value={9}>9</ToggleButton>
                <ToggleButton value={10}>10</ToggleButton>
            </ToggleButtonGroup>
          </DialogContent>
          <DialogActions>
            <Button onClick={() => {props.editLecture(false); setStudentName(''); setGrade(-1)}}>Cancel</Button>
            <Button onClick={() => {props.editLecture(true, studentName, grade); setStudentName(''); setGrade(-1)}}>Apply changes</Button>
          </DialogActions>
        </Dialog>
      </div>
    );
}