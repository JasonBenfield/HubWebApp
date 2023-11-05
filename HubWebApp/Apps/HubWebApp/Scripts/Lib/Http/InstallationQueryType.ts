// Generated code
import { NumericValue } from '@jasonbenfield/sharedwebapp/NumericValue';
import { NumericValues } from '@jasonbenfield/sharedwebapp/NumericValues';

export class InstallationQueryTypes extends NumericValues<InstallationQueryType> {
	constructor(
		public readonly Installed: InstallationQueryType,
		public readonly Current: InstallationQueryType,
		public readonly UpdateAvailable: InstallationQueryType,
		public readonly All: InstallationQueryType
	) {
		super([Installed,Current,UpdateAvailable,All]);
	}
}

export class InstallationQueryType extends NumericValue implements IInstallationQueryType {
	public static readonly values = new InstallationQueryTypes(
		new InstallationQueryType(0, 'Installed'),
		new InstallationQueryType(5, 'Current'),
		new InstallationQueryType(10, 'Update Available'),
		new InstallationQueryType(100, 'All')
	);
	
	private constructor(Value: number, DisplayText: string) {
		super(Value, DisplayText);
	}
	
	equalsAny: (...other: this[] | IInstallationQueryType[] | number[] | string[]) => boolean;
	
	equals: (other: this | IInstallationQueryType | number | string) => boolean;
}