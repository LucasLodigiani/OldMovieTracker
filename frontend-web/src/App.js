import React from "react";
import { BrowserRouter as Router, Routes ,Route } from "react-router-dom";
import Header from "./components/Header";
import Home from "./components/Home";
import Movie from "./components/Movie"
import './App.css';
import Login from "./components/Auth/Login";
import Users from "./components/Users";

const App = () => {
  return (
    <Router>
      <Header />
      <Routes>
        <Route path="/Movie/:id?" element={<Movie />} />
        <Route path="/Users" element={<Users/>} />
        <Route path="/Login" element={<Login />} />
        <Route path="/" element={<Home />} />
      </Routes>
    </Router>
  );
};

export default App;
