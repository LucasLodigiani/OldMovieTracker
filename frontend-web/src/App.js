import React from "react";
import { BrowserRouter as Router, Routes ,Route } from "react-router-dom";
import Header from "./components/Header";
import Layout from "./components/Layout";
import Home from "./components/Home";
import Movie from "./components/Movie"
import './App.css';

const App = () => {
  return (
    <Router>
      <Header />
      <Routes>
        <Route path="/Movie/:id?" element={<Movie />} />
        <Route path="/" element={<Home />} />
      </Routes>
    </Router>
  );
};

export default App;
