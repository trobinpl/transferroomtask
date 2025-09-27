import type { FC } from "react"
import "./Loader.css"

export const Loader: FC = () => {
  return (
    <div className="lds-ring">
      <div></div>
      <div></div>
      <div></div>
      <div></div>
    </div>
  )
}
