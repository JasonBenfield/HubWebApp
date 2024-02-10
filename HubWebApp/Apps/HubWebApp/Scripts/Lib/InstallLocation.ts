
export class InstallLocation {
    readonly id: number;
    readonly qualifiedMachineName: string;

    constructor(source: IInstallLocationModel) {
        this.id = source.ID;
        this.qualifiedMachineName = source.QualifiedMachineName;
    }
}