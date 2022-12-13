import * as React from 'react';
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogContentText from '@mui/material/DialogContentText';
import DialogTitle from '@mui/material/DialogTitle';
import { ToggleButton, ToggleButtonGroup, Typography } from '@mui/material';

export default function CreateGradeDialog(props) {
  const [studentName, setStudentName] = React.useState('')
  const [grade, setGrade] = React.useState(1)

  function handleGradeChange (event: React.MouseEvent<HTMLElement>, newGrade: number,) {
    setGrade(newGrade);
  };

  function handleStudentNameChange(event: React.ChangeEvent<HTMLInputElement>){
    setStudentName(event.target.value)
  }

  return (
    <div>
      <Dialog open={props.isOpen} onClose={() => {props.createGrade(false); setStudentName(''); setGrade(1)}}>
        <DialogTitle>Add a new grade</DialogTitle>
        <DialogContent>
          <DialogContentText>
            Enter the students name
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
          <Button onClick={() => {props.createGrade(false); setStudentName(''); setGrade(1)}}>Cancel</Button>
          <Button onClick={() => {props.createGrade(true, studentName, grade); setStudentName(''); setGrade(1)}}>Create</Button>
        </DialogActions>
      </Dialog>
    </div>
  );
}