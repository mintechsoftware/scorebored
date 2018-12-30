import axios from 'axios';
import {
  auth_header
} from '@/helpers/auth-header';

export const group_services = {
  create_group,
  get_user_groups,
  update_user_current_group_id,
  generate_code,
  join_group
};

function create_group(name) {
  return axios.post(`${process.env.VUE_APP_API_URL}/group/create`, {
    Name: name
  }, {
    headers: auth_header()
  });
}

function get_user_groups(user_id) {
  if (user_id) {
    return axios.get(`${process.env.VUE_APP_API_URL}/user/groups/${user_id}`, {
      headers: auth_header()
    });
  }
}

function update_user_current_group_id(user_id, group_id) {
  return axios.post(`${process.env.VUE_APP_API_URL}/user/groups/current_group_id?userId=${user_id}&groupId=${group_id}`, {}, {
    headers: auth_header()
  });
}

function generate_code(user_id, group_id) {
  return axios.get(`${process.env.VUE_APP_API_URL}/groups/access_code/${user_id}/${group_id}`, {
    headers: auth_header()
  });
}

function join_group(user_id, code) {
  return axios.post(`${process.env.VUE_APP_API_URL}/groups/join/${user_id}`, {
    Code: code
  }, {
    headers: auth_header()
  });
}
