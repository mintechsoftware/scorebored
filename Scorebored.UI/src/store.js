import Vue from 'vue';
import Vuex from 'vuex';

import {
  users
} from '@/store/users.module';
import {
  groups
} from '@/store/groups.module';
import {
  scoreboards
} from '@/store/scoreboards.module';

Vue.use(Vuex);

export const store = new Vuex.Store({
  modules: {
    users,
    groups,
    scoreboards
  }
});
