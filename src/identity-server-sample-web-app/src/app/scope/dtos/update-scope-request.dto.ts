export class UpdateScopeRequestDto {
  public constructor(
    public readonly scopeName  : string,
    public readonly displayName: string,
  ) {}
}
