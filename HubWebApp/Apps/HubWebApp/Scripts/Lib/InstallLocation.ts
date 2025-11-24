
export class InstallLocation {
    readonly id: number;
    readonly qualifiedMachineName: string;

    constructor(source?: IInstallLocationModel) {
        this.id = source ? source.ID : 0;
        this.qualifiedMachineName = source ? source.QualifiedMachineName : "";
    }
}