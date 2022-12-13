import { Button, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle, TextField } from '@mui/material';
import * as React from 'react';

export default function CreateLectureDialog(props){
    const [material, setMaterial] = React.useState('')
    const [assignment, setAssignment] = React.useState('')

    function handleMaterialChange(event: React.ChangeEvent<HTMLInputElement>){
      setMaterial(event.target.value)
    }

    function handleAssignmentChange(event: React.ChangeEvent<HTMLInputElement>){
        setAssignment(event.target.value)
    }
  
    return (
      <div>
        <Dialog open={props.isOpen} onClose={() => {props.createLecture(false); setMaterial(''); setAssignment('')}}>
          <DialogTitle>Create a new lecture</DialogTitle>
          <DialogContent>
            <DialogContentText>
              Please enter the material and the assignment for your new lecture
            </DialogContentText>
            <TextField
              autoFocus
              margin="dense"
              id="name"
              label="Lecture Material"
              type="text"
              fullWidth
              variant="outlined"
              value={material}
              onChange={handleMaterialChange}
              multiline
              minRows={5}
            />
            <TextField
              autoFocus
              margin="dense"
              id="name"
              label="Lecture Assignment"
              type="text"
              fullWidth
              variant="outlined"
              value={assignment}
              onChange={handleAssignmentChange}
              multiline
              minRows={5}
            />
          </DialogContent>
          <DialogActions>
            <Button onClick={() => {props.createLecture(false); setMaterial(''); setAssignment('')}}>Cancel</Button>
            <Button onClick={() => {props.createLecture(true, material, assignment); setMaterial(''); setAssignment('')}}>Create</Button>
          </DialogActions>
        </Dialog>
      </div>
    );
}