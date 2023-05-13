import React, { useState } from 'react';
import User from '../../utils/User';
import { base_url } from '../../utils/Config';


function Login() {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [result, setResult] = useState('');

    const handleSubmit = async (event) => {
        event.preventDefault();
        

        try {
            const response = await fetch(base_url + '/api/Auth/Login', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ username, password })
            });

            if (response.ok) {
                //Tener en cuenta que el servidor no devuelve un objeto json, simplemente devuelve el jwt por eso se lee como text().
                const jwtToken = await response.text();

                // Guardar el jwt en el localStorage
                localStorage.setItem('JWT', jwtToken);

                
                // Reiniciar los campos del form
                setUsername('');
                setPassword('');
                setResult("Inicio de sesion exitoso, jwt guardado en el localstorage!");
            }
            else {
                //Lanzar error para obtenerlo en el catch
                throw new Error('Autenticacion Fallida');
            }
        }
        catch (error) {
            setResult(error.message);

        }
    };

    return (
        <>
            <form onSubmit={handleSubmit}>
                <div>
                    <label>
                        Usuario:
                        <input type="text" value={username} onChange={(event) => setUsername(event.target.value)} />
                    </label>
                </div>
                <div>
                    <label>
                        Contraseña:
                        <input type="password" value={password} onChange={(event) => setPassword(event.target.value)} />
                    </label>
                </div>
                <button type="submit">Iniciar sesión</button>
                <p >{result}</p>
            </form>

            <p>Prueba de vistas condicioandas</p>

            {User.IsInRole("Admin") === true ? <p>Esta en el rol de admin</p> : <p>No esta en el rol de admin</p>}

            {User.IsAuthenticated() === true ? <p>El usuario esta autenticado</p> : <p>El usuario no esta autenticado</p>}
            
            {User.GetUserRole() === "Admin" ? <p>Es admin</p> : <p>Es usuario</p>}
        </>
    );
}

export default Login;
