<template>
  <div class="card p-2 m-4 bg-info borderTrim rounded" style="width: 18rem">
    <div>
      <img
        style="object-fit: contain; width: 100%; max-height: 200px"
        :src="keepData.img"
        class="card-img-top"
        alt="Keep Image..."
        @click="makeActiveKeep(keepData.id), moveToKeepDetails(keepData.id)"
      />
      <div class="card-body">
        <h5 class="card-title">{{ keepData.name }}</h5>
        <p class="card-text">{{ keepData.description }}</p>

        <div class="col-12 d-flex justify-content-between">
          <i class="fa fa-eye p-3"
            >&nbsp;<span class="iconText">{{ keepData.views }}</span></i
          >
          <i class="fa fa-floppy-o p-3"
            >&nbsp;<span class="iconText">{{ keepData.keeps }}</span></i
          >
          <i class="fa fa-share-alt p-3" @click="editKeep"
            >&nbsp;<span class="iconText">{{ keepData.shares }}</span></i
          >
        </div>
      </div>
      <div class="row justify-content-between px-3 pt-2">
        <button
          class="btn btn-sm btn-block btn-info shadow border border-secondary"
          @click="deleteKeep(keepData.id)"
          v-if="keepData.userId == userId"
        >
          <i class="fa fa-trash text-primary"></i> Delete Keep
        </button>
        <button
          class="btn btn-sm btn-block btn-danger shadow mt-2"
          @click="deleteVaultKeepRelationship"
          v-if="vaultKeepRelationshipData"
        >
          Remove From Vault
        </button>
        <i v-if="keepData.isPrivate" class="fa fa-lock">&nbsp; Private</i>
      </div>
    </div>
  </div>
</template>


<script>
export default {
  name: "keepCard",
  props: ["keepData", "vaultKeepRelationshipData"],
  data() {
    return {};
  },
  mounted() {},
  computed: {
    userId() {
      this.$store.dispatch("getUserId");
      return this.$store.state.userId;
    },
  },
  methods: {
    makeActiveKeep(keepId) {
      this.$store.dispatch("getActiveKeep", keepId);
      console.log(keepId);
    },
    moveToKeepDetails(keepId) {
      this.$router.push({ name: "keepDetails", params: { id: keepId } });
    },
    deleteKeep(keepId) {
      this.$store.dispatch("deleteKeep", keepId);
    },
    editKeep() {
      this.$store.dispatch("editKeep", {
        id: this.keepData.id,
        Shares: this.keepData.shares + 1,
        Img: this.keepData.img,
        Description: this.keepData.description,
        Name: this.keepData.name,
        UserId: this.keepData.userId,
      });
    },

    deleteVaultKeepRelationship(keepId) {
      this.$store.dispatch(
        "deleteVaultKeepRelationship",
        this.vaultKeepRelationshipData
      );
    },
  },
  components: {},
};
</script>


