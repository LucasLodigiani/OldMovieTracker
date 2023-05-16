import React, { useState } from 'react';

//TO DO: HACER BIEN ESTO
function NewMovie() {
    const [title, setTile] = useState("");
    const [description, setDescription] = useState("");
    const [releaseDate, setReleaseDate] = useState("");
    const [duration, setDuration] = useState("");
    const [categories, setCategories] = useState([]);

  return (
      <>
          <form>
              <label>Cargar portada:</label>
              <input type="file" name="image"></input>
              <label>Titulo</label>
              <input type="text" name="title"></input>
              <label>Descripcion</label>
              <input type="text" name="description"></input>
              <label>Fecha de estreno</label>
              <input type="text" name="releaseDate"></input>
              <label>Duracion</label>
              <input type="text" name="duration"></input>

              <label>Categorias</label>
              <input type="checkbox" value="Terror"></input>
          </form>
      </>
  );
}

export default NewMovie;