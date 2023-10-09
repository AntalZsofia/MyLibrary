import AuthContext from './../Context/AuthProvider'
import { useContext } from 'react'

export default function useAuth() {
  return useContext(AuthContext)
}