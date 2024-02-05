export const AUTH_CONFIG = {
  routes: {
    router: {
      group: 'auth',
      register: 'register',
      login: 'login',
    },
    api: {
      register: '/auth/register',
      login: '/auth/login',
      refresh: '/auth/refresh',
    },
  },
};
