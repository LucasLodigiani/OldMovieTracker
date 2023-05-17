import React, { useState, useEffect } from "react";
import Nav from "react-bootstrap/Nav";
import { Row, Col } from "react-bootstrap";
import { base_url } from "../utils/Config";

function Home() {
  const [movies, setMovies] = useState([]);

  useEffect(() => {
    getMovies();
  }, []);

  function getMovies() {
    fetch(base_url + "/api/Movies")
      .then((response) => response.json())
      .then((data) => {
        setMovies(data);
        console.log(data)
      })
      .catch((error) => {
        console.error("Error al obtener las películas", error);
      });
  }

  const moviesFiltered = movies.map((m) => (
      <Nav.Link href={`/movie/${m.id}`} key={m.id}>
        <Col >
          <p>{m.title}</p>
          <img src={`${base_url}/Media/${m.mediaUrl}`} alt={`Imagen ${m.id}`} style={{ width: "300px", height: "400px" }} />
        </Col>
      </Nav.Link>
  ));
  console.log(moviesFiltered);
  

  return (
    <>
    <br></br>
    <Row xs={1} sm={2} md={3} lg={4} xl={5} xxl={7}>
      {moviesFiltered}
    </Row>

    </>
  );
}

export default Home;