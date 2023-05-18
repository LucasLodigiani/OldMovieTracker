import React, { useState, useEffect } from "react";
import Container from "react-bootstrap/Container";
import Nav from "react-bootstrap/Nav";
import Navbar from "react-bootstrap/Navbar";
import NavDropdown from "react-bootstrap/NavDropdown";
import User from '../utils/User';
import { base_url } from "../utils/Config";



//<Nav.Link href="#features">Features</Nav.Link>
//<Nav.Link href="#pricing">Pricing</Nav.Link>

function Header() {
  
  const [categories, setCategories] = useState([]);

  useEffect(() => {
    getMovies();
  }, []);

  function getMovies() {
    fetch(base_url + "/api/Categories")
      .then((response) => response.json())
      .then((data) => {
        setCategories(data);
        console.log(data)
      })
      .catch((error) => {
        console.error("Error al obtener las películas", error);
      });
  }

  const handleLogout = () => {
    User.Logout();
  }


  return (
    <Navbar collapseOnSelect expand="lg" bg="dark" variant="dark">
      <Container>
        <Navbar.Brand href="/">MovieTracker</Navbar.Brand>
        <Navbar.Toggle aria-controls="responsive-navbar-nav" />
        <Navbar.Collapse id="responsive-navbar-nav">
          <Nav className="me-auto">
            <NavDropdown title="Categorias" id="collasible-nav-dropdown">
              {categories.map((category) => (
                <NavDropdown.Item key={category.id} href={`#action/${category.id}`}>
                  {category.name}
                </NavDropdown.Item>
              ))}
              <NavDropdown.Divider />
              <NavDropdown.Item href="#action/3.4">
                Separated link
              </NavDropdown.Item>
            </NavDropdown>
          </Nav>
          <Nav>
            {User.IsInRole("Admin") === true ? <Nav.Link href="/Users">Usuarios</Nav.Link> :  null}
            {User.IsAuthenticated() === true ? <Nav.Link href="/" onClick={handleLogout}>Logout</Nav.Link> :  <Nav.Link eventKey={2} href="/login">Login</Nav.Link>}
          </Nav>
        </Navbar.Collapse>
      </Container>
    </Navbar>
  );
}

export default Header;
