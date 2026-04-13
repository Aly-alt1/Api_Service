const API_BASE = 'https://localhost:44350/api/v1/Roles';

async function cargarRoles() {
    const resp = await fetch(API_BASE);
    const data = await resp.json();
    renderTable(data);
}

async function buscarRol() {
    const valor = document.getElementById('searchNombre').value;
    if(!valor) return cargarRoles();
    const resp = await fetch(`${API_BASE}/nombre/${valor}`);
    if(resp.ok) {
        const data = await resp.json();
        renderTable(data ? [data] : []); 
    } else {
        renderTable([]);
    }
}

function renderTable(data) {
    const tbody = document.querySelector('#tablaRoles tbody');
    tbody.innerHTML = '';
    data.forEach(r => {
        tbody.innerHTML += `
            <tr>
                <td>${r.IdRoles}</td>
                <td>${r.StrValor}</td>
                <td>${r.StrDescripcion}</td>
                <td>
                    <button onclick="editar(${r.IdRoles}, '${r.StrValor}', '${r.StrDescripcion}')" class="btn-primary">Editar</button>
                    <button onclick="eliminar(${r.IdRoles})" class="btn-danger">Eliminar</button>
                </td>
            </tr>`;
    });
}

document.getElementById('rolForm').onsubmit = async (e) => {
    e.preventDefault();
    const id = parseInt(document.getElementById('idRol').value);
    const dto = {
        IdRoles: id,
        StrValor: document.getElementById('strValor').value,
        StrDescripcion: document.getElementById('strDescripcion').value
    };

    const method = id === 0 ? 'POST' : 'PUT';
    const url = id === 0 ? `${API_BASE}/Create` : `${API_BASE}/${id}`;

    await fetch(url, {
        method: method,
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(dto)
    });
    resetForm();
    cargarRoles();
};

function editar(id, valor, desc) {
    document.getElementById('idRol').value = id;
    document.getElementById('strValor').value = valor;
    document.getElementById('strDescripcion').value = desc;
    document.getElementById('btnGuardar').innerText = "Actualizar Rol";
}

async function eliminar(id) {
    if (confirm('¿Eliminar este rol del sistema?')) {
        await fetch(`${API_BASE}/${id}`, { method: 'DELETE' });
        cargarRoles();
    }
}

function resetForm() {
    document.getElementById('rolForm').reset();
    document.getElementById('idRol').value = "0";
    document.getElementById('btnGuardar').innerText = "Guardar Rol";
}

window.onload = cargarRoles;