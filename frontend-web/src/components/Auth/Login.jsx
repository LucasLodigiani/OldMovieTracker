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
                <button type="submit">Iniciar sesion</button>
                <p >{result}</p>
            </form>

            <p>Informacion actual:</p>
            {User.IsAuthenticated() === true ? <p>Estas autenticado</p> : <p>No estas autenticado</p>}
        
            {User.GetUserRole() !== false ? <p>Tu rol es {User.GetUserRole()}</p> : null}

        </>
    );
}

export default Login;
