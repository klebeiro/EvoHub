export interface ActionResult<TModel> {
  isValid: boolean
  message: string,
  result: TModel,
}
