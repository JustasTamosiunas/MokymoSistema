import * as React from 'react';
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogContentText from '@mui/material/DialogContentText';
import DialogTitle from '@mui/material/DialogTitle';

export default function CourseCreateDialog(props) {
  const [title, setTitle] = React.useState('')

  function handleTitleChange(event: React.ChangeEvent<HTMLInputElement>){
    setTitle(event.target.value)
  }

  return (
    <div>
      <Dialog open={props.isOpen} onClose={() => {props.createCourse(false); setTitle('')}}>
        <DialogTitle>Create Course</DialogTitle>
        <DialogContent>
          <DialogContentText>
            Please enter the name of the course
          </DialogContentText>
          <TextField
            autoFocus
            margin="dense"
            id="name"
            label="Course Name"
            type="text"
            fullWidth
            variant="standard"
            value={title}
            onChange={handleTitleChange}
          />
        </DialogContent>
        <DialogActions>
          <Button onClick={() => {props.createCourse(false); setTitle('')}}>Cancel</Button>
          <Button onClick={() => {props.createCourse(true, title); setTitle('')}}>Create</Button>
        </DialogActions>
      </Dialog>
    </div>
  );
}