<template>
  <div class="dashboard container-fluid">
    <h1
      class="row p-3 justify-content-center border-top border-dark text-warning"
    >
      My Vaults
    </h1>

    <form
      class="row justify-content-center align-items-center text-warning"
      @submit.prevent="createNewVault"
    >
      <div class="col-2">
        <div class="form-group">
          <label for="newVaultName">New Vault Name</label>
          <input
            type="text"
            class="form-control shadow"
            id="newVaultName"
            placeholder="New Vault Name..."
            v-model="newVaultObject.name"
          />
        </div>
      </div>
      <div class="col-2">
        <div class="form-group">
          <label for="newVaultDescription">New Vault Description</label>
          <input
            type="text"
            class="form-control shadow"
            id="newVaultDescription"
            placeholder="New Vault Description..."
            v-model="newVaultObject.description"
          />
        </div>
      </div>
      <button
        type="submit"
        class="btn btn-sm btn-primary border border-info mt-3 ml-2 shadow"
      >
        Submit
      </button>
    </form>
    <div class="row p-3 justify-content-center">
      <div
        v-for="vault in vaults"
        :key="vault.id"
        class="card bg-info m-3 col-3 borderTrim"
      >
        <div class="card-body">
          <h5 class="card-title">{{ vault.name }}</h5>
          <p class="card-text">{{ vault.description }}</p>
          <div class="row justify-content-between">
            <button
              class="btn btn-sm btn-primary shadow"
              @click="goToVault(vault.id)"
            >
              See Keeps in this Vault
            </button>
            <button
              class="btn btn-sm btn-danger shadow"
              @click="deleteVault(vault.id)"
            >
              Delete
            </button>
          </div>
        </div>
      </div>
    </div>

    <h1
      class="row p-3 justify-content-center mt-2 border-top border-dark text-warning"
    >
      My Keeps
    </h1>
    <form
      class="row justify-content-center align-items-center p-3 mb-4 text-warning"
      @submit.prevent="createNewKeep"
    >
      <div class="col-2">
        <div class="form-group">
          <label for="newKeepName">New Keep Name</label>
          <input
            type="text"
            class="form-control shadow"
            id="newKeepName"
            placeholder="New Keep Name..."
            v-model="newKeepObject.name"
          />
        </div>
      </div>
      <div class="col-2">
        <div class="form-group">
          <label for="newKeepDescription">New Keep Description</label>
          <input
            type="text"
            class="form-control shadow"
            id="newKeepDescription"
            placeholder="New Keep Description..."
            v-model="newKeepObject.description"
          />
        </div>
      </div>
      <div class="col-2">
        <div class="form-group">
          <label for="newKeepImage">New Keep Image URL</label>
          <input
            type="text"
            class="form-control shadow"
            id="newKeepImage"
            placeholder="New Keep Image URL..."
            v-model="newKeepObject.img"
          />
        </div>
      </div>
      <div class>
        <div class="form-check">
          <input
            class="form-check-input shadow"
            type="checkbox"
            id="newKeepPrivateCheck"
            v-model="newKeepObject.isPrivate"
          />
          <label class="form-check-label" for="newKeepPrivateCheck"
            >Make Private?</label
          >
        </div>
      </div>
      <button
        type="submit"
        class="btn btn-sm btn-primary mt-3 ml-3 shadow border border-info"
      >
        Submit
      </button>
    </form>
    <div class="row p-3 justify-content-center">
      <keepCard
        class="m-2"
        v-for="personalKeep in personalKeeps"
        :keepData="personalKeep"
        :key="personalKeep.id"
      />
    </div>
  </div>
</template>

<script>
import keepCard from "../components/keepCard";
export default {
  name: "dashboard",
  data() {
    return {
      newVaultObject: {},
      newKeepObject: {},
    };
  },
  mounted() {
    this.$store.state.vaultKeeps = [];
    this.$store.state.vaultKeepRelationships = [];
    this.$store.dispatch("getMyVaults");
    this.$store.dispatch("getMyKeeps");
  },
  computed: {
    personalKeeps() {
      return this.$store.state.personalKeeps;
    },
    vaults() {
      return this.$store.state.vaults;
    },
  },

  methods: {
    goToVault(vaultId) {
      this.$router.push({ name: "vault", params: { id: vaultId } });
    },
    createNewVault() {
      this.$store.dispatch("createNewVault", {
        name: this.newVaultObject.name,
        description: this.newVaultObject.description,
      });
      this.newVaultObject = {};
    },

    deleteVault(vaultId) {
      this.$store.dispatch("deleteVault", vaultId);
    },

    createNewKeep() {
      this.$store.dispatch("createNewKeep", {
        name: this.newKeepObject.name,
        description: this.newKeepObject.description,
        img: this.newKeepObject.img,
        isPrivate: this.newKeepObject.isPrivate,
      });
      this.newKeepObject = {};
    },
  },

  components: {
    keepCard,
  },
};
</script>


