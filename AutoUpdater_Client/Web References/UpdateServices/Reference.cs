﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace AutoUpdater_Client.UpdateServices {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="UpdateServerSoap", Namespace="http://tempuri.org/")]
    public partial class UpdateServer : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback GetTargetVersionSeqOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetFileListOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetFileOperationCompleted;
        
        private System.Threading.SendOrPostCallback isDirectoryExistsOperationCompleted;
        
        private System.Threading.SendOrPostCallback getUpdateFileNumOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public UpdateServer() {
            this.Url = global::AutoUpdater_Client.Properties.Settings.Default.AutoUpdater_Client_UpdateServices_UpdateServer;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event GetTargetVersionSeqCompletedEventHandler GetTargetVersionSeqCompleted;
        
        /// <remarks/>
        public event GetFileListCompletedEventHandler GetFileListCompleted;
        
        /// <remarks/>
        public event GetFileCompletedEventHandler GetFileCompleted;
        
        /// <remarks/>
        public event isDirectoryExistsCompletedEventHandler isDirectoryExistsCompleted;
        
        /// <remarks/>
        public event getUpdateFileNumCompletedEventHandler getUpdateFileNumCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetTargetVersionSeq", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public VersionModel GetTargetVersionSeq(int versionSeq) {
            object[] results = this.Invoke("GetTargetVersionSeq", new object[] {
                        versionSeq});
            return ((VersionModel)(results[0]));
        }
        
        /// <remarks/>
        public void GetTargetVersionSeqAsync(int versionSeq) {
            this.GetTargetVersionSeqAsync(versionSeq, null);
        }
        
        /// <remarks/>
        public void GetTargetVersionSeqAsync(int versionSeq, object userState) {
            if ((this.GetTargetVersionSeqOperationCompleted == null)) {
                this.GetTargetVersionSeqOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetTargetVersionSeqOperationCompleted);
            }
            this.InvokeAsync("GetTargetVersionSeq", new object[] {
                        versionSeq}, this.GetTargetVersionSeqOperationCompleted, userState);
        }
        
        private void OnGetTargetVersionSeqOperationCompleted(object arg) {
            if ((this.GetTargetVersionSeqCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetTargetVersionSeqCompleted(this, new GetTargetVersionSeqCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetFileList", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public FileModel[] GetFileList(int versionSeq) {
            object[] results = this.Invoke("GetFileList", new object[] {
                        versionSeq});
            return ((FileModel[])(results[0]));
        }
        
        /// <remarks/>
        public void GetFileListAsync(int versionSeq) {
            this.GetFileListAsync(versionSeq, null);
        }
        
        /// <remarks/>
        public void GetFileListAsync(int versionSeq, object userState) {
            if ((this.GetFileListOperationCompleted == null)) {
                this.GetFileListOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetFileListOperationCompleted);
            }
            this.InvokeAsync("GetFileList", new object[] {
                        versionSeq}, this.GetFileListOperationCompleted, userState);
        }
        
        private void OnGetFileListOperationCompleted(object arg) {
            if ((this.GetFileListCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetFileListCompleted(this, new GetFileListCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetFile", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")]
        public byte[] GetFile(RequestFileModel rfm) {
            object[] results = this.Invoke("GetFile", new object[] {
                        rfm});
            return ((byte[])(results[0]));
        }
        
        /// <remarks/>
        public void GetFileAsync(RequestFileModel rfm) {
            this.GetFileAsync(rfm, null);
        }
        
        /// <remarks/>
        public void GetFileAsync(RequestFileModel rfm, object userState) {
            if ((this.GetFileOperationCompleted == null)) {
                this.GetFileOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetFileOperationCompleted);
            }
            this.InvokeAsync("GetFile", new object[] {
                        rfm}, this.GetFileOperationCompleted, userState);
        }
        
        private void OnGetFileOperationCompleted(object arg) {
            if ((this.GetFileCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetFileCompleted(this, new GetFileCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/isDirectoryExists", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool isDirectoryExists(string dir) {
            object[] results = this.Invoke("isDirectoryExists", new object[] {
                        dir});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void isDirectoryExistsAsync(string dir) {
            this.isDirectoryExistsAsync(dir, null);
        }
        
        /// <remarks/>
        public void isDirectoryExistsAsync(string dir, object userState) {
            if ((this.isDirectoryExistsOperationCompleted == null)) {
                this.isDirectoryExistsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnisDirectoryExistsOperationCompleted);
            }
            this.InvokeAsync("isDirectoryExists", new object[] {
                        dir}, this.isDirectoryExistsOperationCompleted, userState);
        }
        
        private void OnisDirectoryExistsOperationCompleted(object arg) {
            if ((this.isDirectoryExistsCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.isDirectoryExistsCompleted(this, new isDirectoryExistsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/getUpdateFileNum", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int getUpdateFileNum(int startSeq, int endSeq) {
            object[] results = this.Invoke("getUpdateFileNum", new object[] {
                        startSeq,
                        endSeq});
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void getUpdateFileNumAsync(int startSeq, int endSeq) {
            this.getUpdateFileNumAsync(startSeq, endSeq, null);
        }
        
        /// <remarks/>
        public void getUpdateFileNumAsync(int startSeq, int endSeq, object userState) {
            if ((this.getUpdateFileNumOperationCompleted == null)) {
                this.getUpdateFileNumOperationCompleted = new System.Threading.SendOrPostCallback(this.OngetUpdateFileNumOperationCompleted);
            }
            this.InvokeAsync("getUpdateFileNum", new object[] {
                        startSeq,
                        endSeq}, this.getUpdateFileNumOperationCompleted, userState);
        }
        
        private void OngetUpdateFileNumOperationCompleted(object arg) {
            if ((this.getUpdateFileNumCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.getUpdateFileNumCompleted(this, new getUpdateFileNumCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class VersionModel {
        
        private int versionIDField;
        
        private string versionNameField;
        
        private string versionSeqField;
        
        private System.DateTime releaseTimeField;
        
        private string statusField;
        
        private string previousVersionNameField;
        
        /// <remarks/>
        public int versionID {
            get {
                return this.versionIDField;
            }
            set {
                this.versionIDField = value;
            }
        }
        
        /// <remarks/>
        public string versionName {
            get {
                return this.versionNameField;
            }
            set {
                this.versionNameField = value;
            }
        }
        
        /// <remarks/>
        public string versionSeq {
            get {
                return this.versionSeqField;
            }
            set {
                this.versionSeqField = value;
            }
        }
        
        /// <remarks/>
        public System.DateTime releaseTime {
            get {
                return this.releaseTimeField;
            }
            set {
                this.releaseTimeField = value;
            }
        }
        
        /// <remarks/>
        public string status {
            get {
                return this.statusField;
            }
            set {
                this.statusField = value;
            }
        }
        
        /// <remarks/>
        public string previousVersionName {
            get {
                return this.previousVersionNameField;
            }
            set {
                this.previousVersionNameField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class RequestFileModel {
        
        private long fileIdField;
        
        private int startPositionField;
        
        private int readFileLengthField;
        
        private string relativePathField;
        
        private string fileNameField;
        
        /// <remarks/>
        public long FileId {
            get {
                return this.fileIdField;
            }
            set {
                this.fileIdField = value;
            }
        }
        
        /// <remarks/>
        public int StartPosition {
            get {
                return this.startPositionField;
            }
            set {
                this.startPositionField = value;
            }
        }
        
        /// <remarks/>
        public int ReadFileLength {
            get {
                return this.readFileLengthField;
            }
            set {
                this.readFileLengthField = value;
            }
        }
        
        /// <remarks/>
        public string RelativePath {
            get {
                return this.relativePathField;
            }
            set {
                this.relativePathField = value;
            }
        }
        
        /// <remarks/>
        public string FileName {
            get {
                return this.fileNameField;
            }
            set {
                this.fileNameField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1586.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class FileModel {
        
        private long fileIdField;
        
        private string fileNameField;
        
        private string relativePathField;
        
        private string fileVersionField;
        
        private string fileVersionSeqField;
        
        private long fileLengthField;
        
        private System.DateTime fileLastTimeField;
        
        /// <remarks/>
        public long FileId {
            get {
                return this.fileIdField;
            }
            set {
                this.fileIdField = value;
            }
        }
        
        /// <remarks/>
        public string FileName {
            get {
                return this.fileNameField;
            }
            set {
                this.fileNameField = value;
            }
        }
        
        /// <remarks/>
        public string RelativePath {
            get {
                return this.relativePathField;
            }
            set {
                this.relativePathField = value;
            }
        }
        
        /// <remarks/>
        public string FileVersion {
            get {
                return this.fileVersionField;
            }
            set {
                this.fileVersionField = value;
            }
        }
        
        /// <remarks/>
        public string FileVersionSeq {
            get {
                return this.fileVersionSeqField;
            }
            set {
                this.fileVersionSeqField = value;
            }
        }
        
        /// <remarks/>
        public long FileLength {
            get {
                return this.fileLengthField;
            }
            set {
                this.fileLengthField = value;
            }
        }
        
        /// <remarks/>
        public System.DateTime FileLastTime {
            get {
                return this.fileLastTimeField;
            }
            set {
                this.fileLastTimeField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    public delegate void GetTargetVersionSeqCompletedEventHandler(object sender, GetTargetVersionSeqCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetTargetVersionSeqCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetTargetVersionSeqCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public VersionModel Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((VersionModel)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    public delegate void GetFileListCompletedEventHandler(object sender, GetFileListCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetFileListCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetFileListCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public FileModel[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((FileModel[])(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    public delegate void GetFileCompletedEventHandler(object sender, GetFileCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetFileCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetFileCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public byte[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((byte[])(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    public delegate void isDirectoryExistsCompletedEventHandler(object sender, isDirectoryExistsCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class isDirectoryExistsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal isDirectoryExistsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    public delegate void getUpdateFileNumCompletedEventHandler(object sender, getUpdateFileNumCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1586.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class getUpdateFileNumCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal getUpdateFileNumCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591