<template>
  <div class="keepDetails container-fluid">
    <div class="row align-items-center p-4">
      <div class="col-md-7 d-flex justify-content-end">
        <div class="card p-2 borderTrim bg-info" style="width: 30rem">
          <img :src="activeKeep.img" class="card-img-top" alt="Keep Image..." />
          <div class="card-body">
            <h5 class="card-title">{{ activeKeep.name }}</h5>
            <p class="card-text">{{ activeKeep.description }}</p>
            <div class="col-12 d-flex justify-content-between">
              <i class="fa fa-eye p-3"
                >&nbsp;<span class="iconText">{{ activeKeep.views }}</span></i
              >
              <i class="fa fa-floppy-o p-3"
                >&nbsp;<span class="iconText">{{ activeKeep.keeps }}</span></i
              >
              <i class="fa fa-share-alt p-3"
                >&nbsp;<span class="iconText">{{ activeKeep.shares }}</span></i
              >
            </div>
            <div class="row justify-content-between px-3 pt-2">
              <button
                class="btn btn-sm btn-block btn-info shadow border border-secondary text-danger"
                v-if="activeKeep.userId == userId"
                @click="deleteKeep(activeKeep.id)"
              >
                <i class="fa fa-trash text-primary"></i>
              </button>
              <i v-if="activeKeep.isPrivate" class="fa fa-lock"
                >&nbsp; Private</i
              >
            </div>
          </div>
        </div>
      </div>
      <div class="col-md-5 d-flex justify-content-start">
        <div class="btn-group">
          <a
            class="btn btn-warning btn-lg dropdown-toggle text-primary my-4"
            type="button"
            data-toggle="dropdown"
            aria-haspopup="true"
            aria-expanded="false"
            >+ Add this Keep to a Vault</a
          >
          <div class="dropdown-menu bg-warning shadow text-primary">
            <a
              v-for="vault in vaults"
              :key="vault.id"
              href="#"
              class="dropdown-item bg-warning text-capitalize text-center text-primary"
              @click="addToVault(vault.id)"
              ><b>{{ vault.name }}</b></a
            >
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<script>
export default {
  name: "keepDetails",
  data() {
    return {};
  },
  mounted() {
    console.log(this.$store.state.activeKeep);
  },
  computed: {
    userId() {
      this.$store.dispatch("getUserId");
      return this.$store.state.userId;
    },

    activeKeep() {
      return this.$store.state.activeKeep;
    },

    vaults() {
      return this.$store.state.vaults;
    },
  },
  methods: {
    addToVault(vaultId) {
      this.$store.dispatch("addToVault", {
        keepId: this.activeKeep.id,
        vaultId: vaultId,
      });
      this.$router.push({ name: "vault", params: { id: vaultId } });
    },
    deleteKeep(keepId) {
      this.$store.dispatch("deleteKeep", keepId);
      this.$router.push({ name: "dashboard" });
    },
  },
  beforeDestroy() {
    this.$store.state.activeKeep = {};
  },
  components: {},
};
</script>


