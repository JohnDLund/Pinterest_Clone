import Vue from "vue";
import Vuex from "vuex";
import Axios from "axios";
import { api } from "./AxiosService.js";
import router from "../router";

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    userId: {},
    publicKeeps: [],
    activeKeep: {},
    personalKeeps: [],
    vaults: [],
    activeVault: {},
    vaultKeeps: [],
    vaultKeepRelationships: []
  },


  mutations: {

    setUserId(state, userId) {
      state.userId = userId
    },
    setPublicKeeps(state, keepsData) {
      state.publicKeeps = keepsData
    },
    setActiveKeep(state, keepData) {
      state.activeKeep = keepData
    },
    setPersonalKeeps(state, keepData) {
      state.personalKeeps = keepData
    },
    setMyVaults(state, vaultData) {
      state.vaults = vaultData
    },
    setActiveVault(state, vaultData) {
      state.activeVault = vaultData
    },
    setVaultKeeps(state, vaultKeepData) {
      state.vaultKeeps = vaultKeepData
    },

    setVaultKeepRelationships(state, vaultKeepData) {
      state.vaultKeepRelationships = vaultKeepData
    }
  },


  actions: {

    async getUserId({ commit }) {
      try {
        let res = await api.get("keeps/userId")
        console.log("Got the UserId", res.data);
        commit("setUserId", res.data)
      } catch (err) {
        console.error(err)
      }
    },


    async getPublicKeeps({ commit }) {
      try {
        let res = await api.get("keeps")
        console.log("Got all public Keeps", res.data);
        commit("setPublicKeeps", res.data)
      } catch (err) {
        console.error(err)
      }
    },

    async getActiveKeep({ commit }, keepId) {
      try {
        let res = await api.get("keeps/" + keepId)
        console.log("Got the Active Keep", res.data);
        commit("setActiveKeep", res.data)
      } catch (err) {
        console.error(err)
      }
    },

    async getMyKeeps({ commit }) {
      try {
        let res = await api.get("keeps/" + "user")
        console.log("Got your personal Keeps", res.data);
        commit("setPersonalKeeps", res.data)
      } catch (err) {
        console.error(err)
      }
    },

    async createNewKeep({ dispatch }, keepObject) {
      try {
        let res = await api.post("keeps", keepObject)
        console.log("Created Keep:", res.data)
        dispatch("getMyKeeps")
      } catch (err) {
        console.error(err)
      }
    },

    async deleteKeep({ dispatch }, keepId) {
      try {
        let res = await api.delete("keeps/" + keepId)
        console.log("Deleted Keep", res.data)
        dispatch("getMyKeeps")
        dispatch("getPublicKeeps")
      } catch (err) {
        console.error(err)
      }
    },

    async editKeep({ dispatch }, keepObject) {
      try {
        let res = await api.put("keeps/" + keepObject.id, keepObject)
        console.log("Edited Keep", res.data)
        dispatch("getPublicKeeps")
      } catch (err) {
        console.error(err)
      }
    },

    async getMyVaults({ commit }) {
      try {
        let res = await api.get("vaults/" + "user")
        console.log("Got Your Vaults", res.data);
        commit("setMyVaults", res.data)
      } catch (err) {
        console.error(err)
      }
    },

    async getVaultById({ commit }, vaultId) {
      try {

        let res = await api.get("vaults/" + vaultId)
        console.log("Got the Vault by ID", res.data);
        commit("setActiveVault", res.data)
      } catch (err) {
        console.error(err)
      }
    },


    async getVaultKeeps({ commit }, vaultId) {
      try {
        let res = await api.get("vaults/" + vaultId + "/keeps")
        console.log("Got the VaultKeeps", res.data);
        commit("setVaultKeeps", res.data)
      } catch (err) {
        console.error(err)
      }
    },

    async getVaultKeepRelationships({ commit }) {
      try {
        let res = await api.get("vaultkeeps")
        console.log("Got the VaultKeepRelationships", res.data);
        commit("setVaultKeepRelationships", res.data)
      } catch (err) {
        console.error(err)
      }
    },
    async deleteVaultKeepRelationship({ dispatch }, vaultKeepData) {
      try {
        let res = await api.delete("vaultkeeps/" + vaultKeepData.id)
        console.log("Deleted the VaultKeepRelationships", res.data);
        dispatch("getVaultKeepRelationships", res.data)
        dispatch("getVaultKeeps", vaultKeepData.vaultId)
      } catch (err) {
        console.error(err)
      }
    },

    async addToVault({ dispatch }, vaultKeepData) {
      try {
        let res = await api.post("vaultkeeps", vaultKeepData)
        console.log("vaultKeeps response:", res.data)
        dispatch("getMyVaults")
        dispatch("getVaultKeeps", vaultKeepData.vaultId)


      } catch (err) {
        console.error(err)
      }
    },

    async createNewVault({ dispatch }, vaultObject) {
      try {
        let res = await api.post("vaults", vaultObject)
        console.log("Created Vault:", res.data)
        dispatch("getMyVaults")
      } catch (err) {
        console.error(err)
      }
    },

    async deleteVault({ dispatch }, vaultId) {
      try {
        let res = await api.delete("vaults/" + vaultId)
        console.log("Deleted Vault", res.data)
        dispatch("getMyVaults")
      } catch (err) {
        console.error(err)
      }
    },


    setBearer({ }, bearer) {
      api.defaults.headers.authorization = bearer;
    },
    resetBearer() {
      api.defaults.headers.authorization = "";
    }
  }
});
