<template>
  <div class="container-fluid home">
    <div class="back">
      <img src="../assets/img/RevillaLooks.png" class=" masonry-with-flex shadow" alt="filler img">
      <div class="keep-title text-light text-right">
        <p>
          keep title
        </p>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'Home'
}
</script>

<style scoped lang="scss">
.home{
  text-align: center;
  user-select: none;
  > img{
    height: 200px;
    width: 200px;
  }
}

.gradient {
  background-image: linear-gradient(to top, rgba(0, 0, 0, 0.281) , transparent);
  background-size: cover;
  z-index: 1;
}

.keep-title {
  position: absolute;
  bottom: 50px;
}

</style>
