const toastTrigger = document.getElementById('noticebtn')
const toastLiveExample = document.getElementById('livenotice')

if (toastTrigger) {
  const toastBootstrap = bootstrap.Toast.getOrCreateInstance(toastLiveExample)
  toastTrigger.addEventListener('click', () => {
    toastBootstrap.show()
  })
}