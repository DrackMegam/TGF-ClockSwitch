import { createRouter, createWebHistory } from 'vue-router'
// https://stackoverflow.com/questions/45296505/how-to-use-vue-router-params
const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: () => import('../components/LoginComponent.vue')
    },
    {
      path: '/main/:username',
      name: 'main',
      component: () => import('../components/MainComponent.vue'),
    },
    {
      path: '/weektime/:username',
      name: 'weektime',
      component: () => import('../components/WeekTimeComponent.vue')
    },
    {
      path: '/everyone/:username',
      name: 'everyone',
      component: () => import('../components/EveryoneComponent.vue')
    },
    {
      path: '/admin/:username',
      name: 'admin',
      component: () => import('../components/AdminComponent.vue')
    },
    {
      path: '/subscription/:username',
      name: 'subscription',
      component: () => import('../components/SubscriptionComponent.vue')
    },
    {
      path: '/profile/:username',
      name: 'profile',
      component: () => import('../components/ProfileComponent.vue')
    },
  ]
})

export default router
