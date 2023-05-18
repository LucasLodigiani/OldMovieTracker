import React, { useState, useEffect } from 'react'
import Alert from 'react-bootstrap/Alert';
import Button from 'react-bootstrap/Button';
import { base_url } from "../utils/Config";

const Users = () => {
    const [users, setUsers] = useState([]);


    useEffect(() => {
        getUsers();
      }, []);
    
    function getUsers() {
        fetch(base_url + "/api/Users/GetAllUsers")
          .then((response) => response.json())
          .then((data) => {
            setUsers(data);
            console.log(data)
          })
          .catch((error) => {
            console.error("Error al obtener los usuarios", error);
          });
      }

    const usersFiltered = users.map((u) => (
        <Alert key={u.id} variant="primary">
            <Button variant="secondary">Editar</Button><Button variant="danger">Eliminar</Button> 
            <b>Username</b>:{u.username}  <b>Id</b>:{u.id}
        </Alert>
    ));
  return (
    <>
        <h1>Usuarios:</h1>
        {usersFiltered}
    </>
  )
}
export default Users;