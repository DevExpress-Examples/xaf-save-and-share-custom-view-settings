Imports System.ComponentModel
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.DC
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.ExpressApp.Updating
Imports DevExpress.ExpressApp.Xpo
Imports dxTestSolution.Module.DatabaseUpdate

Namespace ViewSettings.Module

    ' For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.ModuleBase.
    Public NotInheritable Class ViewSettingsModule
        Inherits ModuleBase

        Public Sub New()
            ' 
            ' ViewSettingsModule
            ' 
            AdditionalExportedTypes.Add(GetType(ModelDifference))
            AdditionalExportedTypes.Add(GetType(ModelDifferenceAspect))
            RequiredModuleTypes.Add(GetType(SystemModule.SystemModule))
            RequiredModuleTypes.Add(GetType(Security.SecurityModule))
            RequiredModuleTypes.Add(GetType(Objects.BusinessClassLibraryCustomizationModule))
            RequiredModuleTypes.Add(GetType(ConditionalAppearance.ConditionalAppearanceModule))
            RequiredModuleTypes.Add(GetType(Validation.ValidationModule))
            RequiredModuleTypes.Add(GetType(ViewVariantsModule.ViewVariantsModule))
        End Sub

        Public Overrides Function GetModuleUpdaters(ByVal objectSpace As IObjectSpace, ByVal versionFromDB As Version) As IEnumerable(Of ModuleUpdater)
            Dim updater As ModuleUpdater = New DatabaseUpdate.Updater(objectSpace, versionFromDB)
            Return New ModuleUpdater() {updater, New MyUpdater(objectSpace, versionFromDB)}
        End Function

        Public Overrides Sub Setup(ByVal application As XafApplication)
            MyBase.Setup(application)
        ' Manage various aspects of the application UI and behavior at the module level.
        End Sub

        Public Overrides Sub CustomizeTypesInfo(ByVal typesInfo As ITypesInfo)
            MyBase.CustomizeTypesInfo(typesInfo)
            CalculatedPersistentAliasHelper.CustomizeTypesInfo(typesInfo)
        End Sub
    End Class
End Namespace
