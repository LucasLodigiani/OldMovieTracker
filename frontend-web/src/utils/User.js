

class User {
    static GetUserRole() {
        const jwtToken = localStorage.getItem('JWT')
        if (jwtToken === null) {
            return false
        }

        const parts = jwtToken.split('.');
        const decodedPayload = JSON.parse(atob(parts[1]));
        const { role } = decodedPayload;
        console.log(role);
        return role;
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
            console.log('El token sigue siendo válido.');
            return true;
        }
    }
}

export default User;