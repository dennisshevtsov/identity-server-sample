export class ScopeDto {
  public constructor(
    public readonly scopeName  : string,
    public readonly displayName: string,
  ) { }
}

export class GetScopesResponseDto {
  public constructor(
    public readonly scopes: ScopeDto[],
  ) { }
}
