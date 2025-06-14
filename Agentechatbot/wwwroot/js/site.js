// Funciones globales para manejo de temas

document.addEventListener('DOMContentLoaded', () => {
    const themeStylesheet = document.getElementById('themeStylesheet');
    const themeToggle = document.getElementById('themeToggle');

    window.setTheme = function(theme) {
        if (!themeStylesheet) return;

        if (theme === 'dark') {
            document.body.classList.add('dark-theme');
            document.body.classList.remove('light-theme');
            themeStylesheet.href = '/css/dark-theme.css';
            if (themeToggle) themeToggle.innerHTML = '<i class="fa-solid fa-sun"></i>';
        } else {
            document.body.classList.add('light-theme');
            document.body.classList.remove('dark-theme');
            themeStylesheet.href = '/css/light-theme.css';
            if (themeToggle) themeToggle.innerHTML = '<i class="fa-solid fa-moon"></i>';
        }

        localStorage.setItem('preferred-theme', theme);
    };

    const savedTheme = localStorage.getItem('preferred-theme') || 'light';
    window.setTheme(savedTheme);

    themeToggle?.addEventListener('click', () => {
        const current = document.body.classList.contains('dark-theme') ? 'dark' : 'light';
        window.setTheme(current === 'dark' ? 'light' : 'dark');
    });
});
