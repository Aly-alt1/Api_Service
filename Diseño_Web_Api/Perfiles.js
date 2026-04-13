const API_BASE = 'https://localhost:44350/api/v1';

async function cargarPerfiles() {
    const resp = await fetch(`${API_BASE}/PerfilUsuarios/All`);
    const perfiles = await resp.json();
    const tbody = document.querySelector('#tablaPerfiles tbody');
    tbody.innerHTML = '';

    perfiles.forEach(p => {
        const dir = p.DireccionDto[0] || {};
        const tel = p.TelefonoDto[0] || {};
        tbody.innerHTML += `
            <tr>
                <td>${p.Nombre} ${p.ApellidoPaterno}</td>
                <td>${p.Rfc}</td>
                <td>${new Date(p.FechaNacimiento).toLocaleDateString()}</td>
                <td>${dir.Calle || 'Sin calle'}, Col. ${dir.Colonia || ''}</td>
                <td>${tel.Celular || 'N/A'}</td>
                <td>
                    <button onclick="eliminarPerfil(${p.IdPerfilUsuario})" class="btn-danger">Eliminar</button>
                </td>
            </tr>`;
    });
}

document.getElementById('perfilForm').onsubmit = async (e) => {
    e.preventDefault();
    const perfilData = {
        Nombre: document.getElementById('nombre').value,
        ApellidoPaterno: document.getElementById('apellidoPaterno').value,
        ApellidoMaterno: document.getElementById('apellidoMaterno').value,
        Rfc: document.getElementById('rfc').value,
        FechaNacimiento: document.getElementById('fechaNacimiento').value,
        IdUsuario: document.getElementById('idUsuario').value,
        DireccionDto: [{
            Calle: document.getElementById('calle').value,
            Colonia: document.getElementById('colonia').value,
            NumExterior: document.getElementById('numExt').value,
            Municipio: document.getElementById('municipio').value
        }],
        TelefonoDto: [{
            Celular: document.getElementById('celular').value
        }]
    };

    const resp = await fetch(`${API_BASE}/PerfilUsuarios/Create`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(perfilData)
    });

    if (resp.ok) {
        alert('Perfil creado exitosamente');
        document.getElementById('perfilForm').reset();
        cargarPerfiles();
    }
};

async function eliminarPerfil(id) {
    if (confirm('¿Seguro que desea eliminar el perfil, su dirección y teléfono?')) {
        await fetch(`${API_BASE}/PerfilUsuarios/${id}`, { method: 'DELETE' });
        cargarPerfiles();
    }
}

function logout() { localStorage.clear(); window.location.href = 'login.html'; }

window.onload = cargarPerfiles;