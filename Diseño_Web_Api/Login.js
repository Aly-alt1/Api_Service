const API_BASE = 'https://localhost:44350/api/v1';

document.getElementById('loginForm').onsubmit = async (e) => {
    e.preventDefault();
    const loginInfo = { 
        Username: document.getElementById('username').value, 
        Password: document.getElementById('password').value 
    };

    try {
        const resp = await fetch(`${API_BASE}/Usuarios/Login`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(loginInfo)
        });

        if (resp.ok) {
            const result = await resp.json();
            localStorage.setItem('session', JSON.stringify(result.Usuario));
            window.location.href = 'perfiles.html';
        } else {
            alert('Acceso denegado: Usuario suspendido o credenciales incorrectas.');
        }
    } catch (error) {
        console.error('Error en el login:', error);
        alert('No se pudo conectar con el servidor.');
    }
};