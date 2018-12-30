import axios from 'axios';
import {
  auth_header
} from '@/helpers/auth-header';

export const user_services = {
  login,
  logout,
  register,
  get_user_data
};

function register(email, password) {
  return axios.post(`${process.env.VUE_APP_API_URL}/users/register`, {
    Email: email,
    Password: password
  });
}

function login(email, password) {
  return axios.post(`${process.env.VUE_APP_API_URL}/users/authenticate`, {
    Email: email,
    Password: password
  });
}

function logout() {
  localStorage.removeItem('user');
}

function get_user_data(user_id) {
  return axios.get(`${process.env.VUE_APP_API_URL}/user/${user_id}`, {
    headers: auth_header()
  });
}
