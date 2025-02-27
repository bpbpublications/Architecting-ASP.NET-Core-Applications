window.authManager = {
    getToken: function () {
        return localStorage.getItem('authToken');
    },
    setToken: function (token) {
        localStorage.setItem('authToken', token);
    },
    removeToken: function () {
        localStorage.removeItem('authToken');
    }
};
