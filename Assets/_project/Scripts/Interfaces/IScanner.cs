using System;
using System.Collections;

public interface IScanner
{
    public event Action<Resource> ResourceFound;
    public bool IsScanning { get; set; }
    public void StartScan();
    public IEnumerator ScanningProcess();
    public void PerformScan();
    public void UpdateScanRadius();
    public void StopScan();
}