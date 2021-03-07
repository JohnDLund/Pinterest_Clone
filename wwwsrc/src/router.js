import Vue from "vue";
import Router from "vue-router";
import Home from "./views/Home.vue";
import Dashboard from "./views/Dashboard.vue";
import KeepDetails from "./views/KeepDetails.vue"
import Vault from "./views/Vault.vue"
import { authGuard } from "@bcwdev/auth0-vue";

Vue.use(Router);

export default new Router({
  routes: [
    {
      path: "/",
      name: "home",
      component: Home
    },
    {
      path: "/dashboard",
      name: "dashboard",
      component: Dashboard,
      beforeEnter: authGuard
    },
    {
      path: "/keepDetails",
      name: "keepDetails",
      component: KeepDetails,
      beforeEnter: authGuard
    },
    {
      path: "/vaults/:id",
      name: "vault",
      component: Vault,
      beforeEnter: authGuard
    },
  ]
});
