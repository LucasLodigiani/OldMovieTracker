import React, { useState } from "react";
import { base_url } from "../utils/Config";

function Home() {
  const { movies, setMovies } = useState("");

  function getMovies() {
    const response = fetch(base_url + "/api/Movies")
      .then((response) => response.json())
      .then((data) => {
        console.log(data);
      })
      .catch((error) => {
        console.error("Error al obtener las pel�culas", error);
      });

    return response;
  }

  console.log(getMovies());

  return (
    <>
      <h1>hola</h1>
    </>
  );
}

export default Home;
