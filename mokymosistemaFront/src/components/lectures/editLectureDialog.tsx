import { Button, Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle, TextField } from '@mui/material';
import * as React from 'react';

export default function EditLectureDialog(props){
    const [material, setMaterial] = React.useState(props.material)
    const [assignment, setAssignment] = React.useState(props.assignment)

    React.useEffect(() => {
      setAssignment(props.assignment)
      setMaterial(props.material)
    }, [props.assignment, props.material])

    function handleMaterialChange(event: React.ChangeEvent<HTMLInputElement>){
      setMaterial(event.target.value)
    }

    function handleAssignmentChange(event: React.ChangeEvent<HTMLInputElement>){
        setAssignment(event.target.value)
    }
  
    return (
      <div>
        <Dialog open={props.isOpen} onClose={() => {props.editLecture(false); setMaterial(''); setAssignment('')}}>
          <DialogTitle>Edit lecture</DialogTitle>
          <DialogContent>
            <DialogContentText>
              Please change the material and the assignment for the lecture
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
            <Button onClick={() => {props.editLecture(false); setMaterial(''); setAssignment('')}}>Cancel</Button>
            <Button onClick={() => {props.editLecture(true, material, assignment); setMaterial(''); setAssignment('')}}>Apply changes</Button>
          </DialogActions>
        </Dialog>
      </div>
    );
}