import './App.css';
import Courses from './components/courses/courses';
import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import { ThemeProvider, createTheme } from '@mui/material/styles';
import CssBaseline from '@mui/material/CssBaseline';
import Login from './components/auth/Login';
import { useState } from 'react';
import useToken from './components/auth/usetoken';
import DrawerAppBar from './components/shared/header';
import Lectures from './components/lectures/lectures';
import '@fontsource/roboto/300.css';
import '@fontsource/roboto/400.css';
import '@fontsource/roboto/500.css';
import '@fontsource/roboto/700.css';
import Grades from './components/grades/grades';

const darkTheme = createTheme({
  palette: {
    mode: 'dark',
  },
});

function App() {
  const { token, setToken } = useToken();

  if(!token) {
    return (
      <ThemeProvider theme={darkTheme}>
        <CssBaseline/>
        <div className="App">
          <header className="App-header">
            <Login setToken={setToken}/>
          </header>
        </div>
      </ThemeProvider>
    )
  }

  return (
    <ThemeProvider theme={darkTheme}>
      <CssBaseline />
      <div className="App">
        <header className="App-header">
          <DrawerAppBar />
          <BrowserRouter>
            <Routes>
              <Route path="/courses" element={<Courses />}/>
              <Route path='/courses/:courseId/lectures' element={<Lectures />}/>
              <Route path='/courses/:courseId/lectures/:lectureId/grades' element={<Grades />}/>
              <Route path="*" element={<Navigate to="/courses" replace />} />
            </Routes>
          </BrowserRouter>
        </header>
      </div>
    </ThemeProvider>
  );
}

export default App;
