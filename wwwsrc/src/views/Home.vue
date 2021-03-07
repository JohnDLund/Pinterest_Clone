<template>
  <div class="home container-fluid">
    <h1
      class="row p-3 justify-content-center border-top border-dark text-warning"
    >
      Public Keeps
    </h1>
    <div class="row p-3 justify-content-center">
      <keepCard
        class="m-2"
        v-for="publicKeep in publicKeeps"
        :keepData="publicKeep"
        :key="publicKeep.id"
      />
    </div>
  </div>
</template>

<script>
import keepCard from "../components/keepCard";

export default {
  name: "home",
  mounted() {
    this.$store.state.vaultKeeps = [];
    this.$store.state.vaultKeepRelationships = [];
    this.$store.dispatch("getMyVaults");
    this.$store.dispatch("getPublicKeeps");
  },
  computed: {
    publicKeeps() {
      return this.$store.state.publicKeeps;
    },
    vaults() {
      return this.$store.state.vaults;
    },
  },
  methods: {
    logout() {
      this.$store.dispatch("logout");
    },
  },
  components: {
    keepCard,
  },
};
</script>
