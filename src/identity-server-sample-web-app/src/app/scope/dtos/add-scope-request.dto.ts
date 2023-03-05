export class AddScopeRequestDto {
  public constructor(
    public readonly scopeName: string,
    public readonly displayName: string,
  ) { }
}
