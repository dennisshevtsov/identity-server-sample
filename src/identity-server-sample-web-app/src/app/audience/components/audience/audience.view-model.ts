export class AudienceViewModel {
  public constructor(
    public audienceName: string   = '',
    public displayName : string   = '',
    public description : string   = '',
    public scopes      : string[] = [],
  ) {}
}
