<template>
  <div class="vault container-fluid">
    <h1
      class="row p-3 justify-content-center border-top border-dark text-warning"
    >
      {{ activeVault.name }}
    </h1>
    <div class="row p-3">
      <keepCard
        class="m-2"
        v-for="vaultKeep in vaultKeeps"
        :keepData="vaultKeep"
        :vaultKeepRelationshipData="vaultKeepRelationship"
        :key="vaultKeep.vaultKeepId"
      />
    </div>
  </div>
</template>

<script>
import keepCard from "../components/keepCard";

export default {
  name: "vault",
  mounted() {
    this.$store.dispatch("getVaultKeepRelationships");
    this.$store.dispatch("getVaultById", this.$route.params.id);
    this.$store.dispatch("getVaultKeeps", this.$route.params.id);
  },
  computed: {
    activeVault() {
      console.log("THISSSS", this.$store.state.activeVault);
      return this.$store.state.activeVault;
    },
    vaultKeeps() {
      return this.$store.state.vaultKeeps;
    },
    vaultKeepRelationship() {
      let foundVaultKeepRelationship = this.$store.state.vaultKeepRelationships.find(
        (relationship) => relationship.vaultId == this.$route.params.id
      );
      return foundVaultKeepRelationship;
    },
  },
  methods: {},
  beforeDestroy() {
    this.$store.state.activeVault = {};
  },
  components: {
    keepCard,
  },
};
</script>