const API_URL = 'https://localhost:44350/api/v1/Usuarios/Create';

document.getElementById('registroForm').onsubmit = async (e) => {
    e.preventDefault();

    const username = document.getElementById('username').value;
    const pass = document.getElementById('password').value;
    const confirmPass = document.getElementById('confirmPassword').value;

    if (pass !== confirmPass) {
        alert("Las contraseñas no coinciden. Por favor, verifíquelas.");
        return;
    }

    const nuevoUsuario = {
        Username: username,
        Password: pass,
        Suspendido: false,
        UsuarioRolDto: [] 
    };

    try {
        const response = await fetch(API_URL, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(nuevoUsuario)
        });

        if (response.ok) {
            alert("¡Usuario creado exitosamente! Ahora puede iniciar sesión.");
            window.location.href = 'login.html';
        } else {
            const error = await response.text();
            alert("Error al registrar: " + error);
        }
    } catch (err) {
        console.error("Error de conexión:", err);
        alert("No se pudo conectar con el servidor.");
    }
};