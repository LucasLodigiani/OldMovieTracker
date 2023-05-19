

class User {
    /*Vistas condicionadas

    {User.IsInRole("Admin") === true ? <p>Actualmente estas en el rol de admin</p> : <p>Actualmente no estas en el rol de admin</p>}

    {User.IsAuthenticated() === true ? <p>Estas autenticado</p> : <p>No estas autenticado</p>}
    
    {User.GetUserRole() === "Admin" ? <p>Tu rol es admin</p> : <p>Tu rol es usuario u otro</p>}
    */
    static DecodeToken(){
        const jwtToken = localStorage.getItem('JWT')
        if (jwtToken === null) {
            return false
        }
        const parts = jwtToken.split('.');
        const decodedPayload = JSON.parse(atob(parts[1]));
        return decodedPayload;
    }

    static GetUserRole() {
        const jwtToken = localStorage.getItem('JWT')
        if (jwtToken === null) {
            return false
        }

        const parts = jwtToken.split('.');
        const decodedPayload = JSON.parse(atob(parts[1]));
        console.log(decodedPayload);
        const { role } = decodedPayload;
        console.log(role);
        return role;
    }

    static GetUserName(){
        const { unique_name } = this.DecodeToken();
        return unique_name;
    }


    static IsInRole(role) {
        if (this.GetUserRole() === role && this.GetUserRole !== null) {
            return true;
        }
        else {
            return false;
        }
    }



    static IsAuthenticated() {
        const jwtToken = localStorage.getItem('JWT');
        if (jwtToken === null) {
            return false
        }
        const parts = jwtToken.split('.');
        const decodedPayload = JSON.parse(atob(parts[1]));
        const { exp } = decodedPayload;
        const expDate = new Date(exp * 1000);
        let dateNow = new Date();

        if (expDate < dateNow) {
            console.log('El token ha expirado.');
            localStorage.removeItem('JWT');
            return false;
        } else {
            console.log('El token sigue siendo v�lido.');
            return true;
        }
    }

    static Logout(){
        localStorage.removeItem('JWT');
    }
}

export default User;