import React, { useState, useEffect } from "react";
import { useParams} from "react-router-dom";
import { base_url } from "../utils/Config";


function Movie() {
    const [movie,setMovie] = useState("");

    const {id} = useParams();
    //Evita que se haga una solicitud a la api si el id no esta definido, el return vacío es para salir del hook.
    useEffect(() => {
      if (id === null || id === undefined) {
        return;
      }
      getMovie();
    }, [id]);

    if (id === null || id === undefined) {
      return <p>404 Not Found</p>;
    }
    
  
    function getMovie() {
      fetch(base_url + "/api/Movies/" + id)
        .then((response) => response.json())
        .then((data) => {
          setMovie(data);
          console.log(data)
        })
        .catch((error) => {
          console.error("Error al obtener las películas", error);
        });
    }
    
    
    return(
    <>
      <h1>{movie.title}</h1>
      <img src={`${base_url}/Media/${movie.mediaUrl}`} alt={`Imagen ${movie.id}`} style={{ width: "300px", height: "400px" }} />
      <h2>{movie.description}</h2>
      <h3>release date: {movie.releaseDate}</h3>
      <h3>duration: {movie.duration}</h3>
      <h3>rate: {movie.rate}</h3>
    </>)
}

export default Movie;

