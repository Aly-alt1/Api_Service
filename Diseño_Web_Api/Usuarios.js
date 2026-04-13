const API_USERS = 'https://localhost:44350/api/v1/Usuarios';
const API_ROLES = 'https://localhost:44350/api/v1/Roles';

async function cargarCatalogosRoles() {
    const resp = await fetch(API_ROLES);
    const roles = await resp.json();
    const container = document.getElementById('rolesCheckboxes');
    container.innerHTML = '';
    roles.forEach(r => {
        container.innerHTML += `
            <label>
                <input type="checkbox" name="rolSelect" value="${r.IdRoles}"> 
                ${r.StrValor}
            </label>`;
    });
}

async function cargarUsuarios() {
    const resp = await fetch(`${API_USERS}/All`);
    const data = await resp.json();
    const tbody = document.querySelector('#tablaUsuarios tbody');
    tbody.innerHTML = '';
    data.forEach(u => {
        const rolesNombres = (u.UsuarioRolDto || []).map(ur => ur.RolesDto?.StrValor).join(', ') || 'Sin roles';
        const estado = u.Suspendido ? '<span style="color:red">Suspendido</span>' : '<span style="color:green">Activo</span>';
        const rolesIds = (u.UsuarioRolDto || []).map(ur => ur.IdRoles);

        tbody.innerHTML += `
            <tr>
                <td>${u.IdUser}</td>
                <td>${u.Username}</td>
                <td>${estado}</td>
                <td>${rolesNombres}</td>
                <td>
                    <button onclick="editar(${u.IdUser}, '${u.Username}', '${u.Password}', ${u.Suspendido}, [${rolesIds}])" class="btn-primary">Editar</button>
                    <button onclick="eliminar(${u.IdUser})" class="btn-danger">Eliminar</button>
                </td>
            </tr>`;
    });
}

document.getElementById('userForm').onsubmit = async (e) => {
    e.preventDefault();
    const id = parseInt(document.getElementById('idUser').value);
    
    const rolesSeleccionados = Array.from(document.querySelectorAll('input[name="rolSelect"]:checked')).map(cb => {
        return { IdRoles: parseInt(cb.value) };
    });

    const dto = {
        IdUser: id,
        Username: document.getElementById('username').value,
        Password: document.getElementById('password').value,
        Suspendido: document.getElementById('suspendido').checked,
        UsuarioRolDto: rolesSeleccionados
    };

    const method = id === 0 ? 'POST' : 'PUT';
    const url = id === 0 ? `${API_USERS}/Create` : `${API_USERS}/${id}`;

    await fetch(url, {
        method: method,
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(dto)
    });
    resetForm();
    cargarUsuarios();
};

function editar(id, username, password, suspendido, rolesIds) {
    document.getElementById('idUser').value = id;
    document.getElementById('username').value = username;
    document.getElementById('password').value = password;
    document.getElementById('suspendido').checked = suspendido;
    document.getElementById('btnGuardar').innerText = "Actualizar Usuario";

    document.querySelectorAll('input[name="rolSelect"]').forEach(cb => {
        cb.checked = rolesIds.includes(parseInt(cb.value));
    });
}

async function eliminar(id) {
    if (confirm('¿Eliminar este usuario y sus permisos?')) {
        await fetch(`${API_USERS}/${id}`, { method: 'DELETE' });
        cargarUsuarios();
    }
}

function resetForm() {
    document.getElementById('userForm').reset();
    document.getElementById('idUser').value = "0";
    document.getElementById('btnGuardar').innerText = "Guardar Usuario y Roles";
    document.querySelectorAll('input[name="rolSelect"]').forEach(cb => cb.checked = false);
}

window.onload = async () => {
    await cargarCatalogosRoles(); 
    cargarUsuarios();             
};