import * as React from 'react';
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogContentText from '@mui/material/DialogContentText';
import DialogTitle from '@mui/material/DialogTitle';

export default function CourseEditDialog(props) {
  const [title, setTitle] = React.useState('')

  function handleTitleChange(event: React.ChangeEvent<HTMLInputElement>){
    setTitle(event.target.value)
  }

  return (
    <div>
      <Dialog open={props.isOpen} onClose={() => {props.editCourse(false); setTitle('')}}>
        <DialogTitle>Edit Course</DialogTitle>
        <DialogContent>
          <DialogContentText>
            Please enter the new name of the course
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
          <Button onClick={() => {props.editCourse(false); setTitle('')}}>Cancel</Button>
          <Button onClick={() => {props.editCourse(true, title); setTitle('')}}>Apply Changes</Button>
        </DialogActions>
      </Dialog>
    </div>
  );
}