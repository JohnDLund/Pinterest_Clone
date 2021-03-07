<template>
  <div
    class="card p-2 shadow border border-dark"
    style="width: 18rem"
    @click="
      makeActiveKeep(vaultKeepData.id), moveToKeepDetails(vaultKeepData.id)
    "
  >
    <img :src="vaultKeepData.img" class="card-img-top" alt="Keep Image..." />
    <div class="card-body">
      <h5 class="card-title">{{ vaultKeepData.name }}</h5>
      <p class="card-text">{{ vaultKeepData.description }}</p>
      <div class="col-12 d-flex justify-content-between">
        <i class="fa fa-eye p-3">&nbsp;{{ vaultKeepData.views }}</i>
        <i class="fa fa-floppy-o p-3">&nbsp;{{ vaultKeepData.keeps }}</i>
        <i class="fa fa-share-alt p-3">&nbsp;{{ vaultKeepData.shares }}</i>
      </div>
    </div>
  </div>
</template>


<script>
export default {
  name: "vaultKeepCard",
  props: ["vaultKeepData"],
  data() {
    return {};
  },
  mounted() {},
  computed: {},
  methods: {
    makeActiveKeep(keepId) {
      this.$store.dispatch("getActiveKeep", keepId);
      console.log(keepId);
    },
    moveToKeepDetails(keepId) {
      this.$router.push({ name: "keepDetails", params: { id: keepId } });
    },
  },
  components: {},
};
</script>


<style scoped>
</style>